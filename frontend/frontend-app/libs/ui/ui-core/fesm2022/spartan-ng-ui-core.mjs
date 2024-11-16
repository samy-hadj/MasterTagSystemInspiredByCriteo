import { InjectionToken, forwardRef, inject, ChangeDetectorRef, ElementRef } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { debounceTime, tap, fromEvent, of, switchMap, filter, merge, Observable, pipe } from 'rxjs';
import { clsx } from 'clsx';
import { twMerge } from 'tailwind-merge';

function createInjectionToken(description) {
    const token = new InjectionToken(description);
    const provideFn = (value) => {
        return { provide: token, useValue: value };
    };
    const provideExistingFn = (value) => {
        return { provide: token, useExisting: forwardRef(value) };
    };
    const injectFn = (options = {}) => {
        return inject(token, options);
    };
    return [injectFn, provideFn, provideExistingFn, token];
}

const [injectCustomClassSettable, provideCustomClassSettable, provideCustomClassSettableExisting, SET_CLASS_TO_CUSTOM_ELEMENT_TOKEN,] = createInjectionToken('@spartan-ng SET_CLASS_TO_CUSTOM_ELEMENT_TOKEN');

const [injectExposedSideProvider, provideExposedSideProvider, provideExposedSideProviderExisting, EXPOSES_SIDE_TOKEN,] = createInjectionToken('@spartan-ng EXPOSES_SIDE_TOKEN');

const [injectExposesStateProvider, provideExposesStateProvider, provideExposesStateProviderExisting, EXPOSES_STATE_TOKEN,] = createInjectionToken('@spartan-ng EXPOSES_STATE_TOKEN');

function rxHostListener(event) {
    const cdr = inject(ChangeDetectorRef);
    // Listen to event
    return fromEvent(inject(ElementRef).nativeElement, event).pipe(debounceTime(0), tap(() => cdr.markForCheck()), // Trigger CD like @HostListener would
    takeUntilDestroyed());
}
function rxHostBinding(prop, stream) {
    // Listen to the stream
    stream
        .pipe(takeUntilDestroyed()) // Unsubscribe
        .subscribe(process(inject(ElementRef).nativeElement, prop)); // Process
}
function process(element, prop) {
    const isAttr = prop.startsWith('attr.');
    const isStyle = prop.startsWith('style.');
    const isClass = prop.startsWith('class.');
    const [key, unit = ''] = prop.replace('attr.', '').replace('style.', '').replace('class.', '').split('.');
    return (value) => {
        const parsed = unit && value != null ? `${value}${unit}` : value;
        if (isAttr) {
            if (value == null) {
                element.removeAttribute(key);
            }
            else {
                element.setAttribute(key, String(parsed));
            }
        }
        else if (isClass) {
            element.classList.toggle(key, !!value);
        }
        else if (isStyle) {
            // eslint-disable-next-line @typescript-eslint/ban-ts-comment
            // @ts-ignore
            element.style[key] = parsed;
        }
        else {
            // eslint-disable-next-line @typescript-eslint/ban-ts-comment
            // @ts-ignore
            element[key] = parsed;
        }
    };
}
function rxHostPressedListener() {
    return merge(rxHostListener('click'), rxHostListener('keyup').pipe(switchMap((x) => {
        return x.code === 'Space' || x.code === 'Enter' ? of(true) : of(null);
    }), filter(Boolean))).pipe(debounceTime(0));
}

const [injectTableClassesSettable, provideTableClassesSettable, provideTableClassesSettableExisting, SET_TABLE_CLASSES_TOKEN,] = createInjectionToken('@spartan-ng SET_TABLE_CLASSES_TOKEN');

function brnZoneFull(zone) {
    return (source) => new Observable((subscriber) => source.subscribe({
        next: (value) => zone.run(() => subscriber.next(value)),
        error: (error) => zone.run(() => subscriber.error(error)),
        complete: () => zone.run(() => subscriber.complete()),
    }));
}
function brnZoneFree(zone) {
    return (source) => new Observable((subscriber) => zone.runOutsideAngular(() => source.subscribe(subscriber)));
}
function brnZoneOptimized(zone) {
    return pipe(brnZoneFree(zone), brnZoneFull(zone));
}

function hlm(...inputs) {
    return twMerge(clsx(inputs));
}

/**
 * Generated bundle index. Do not edit.
 */

export { EXPOSES_SIDE_TOKEN, EXPOSES_STATE_TOKEN, SET_CLASS_TO_CUSTOM_ELEMENT_TOKEN, SET_TABLE_CLASSES_TOKEN, brnZoneFree, brnZoneFull, brnZoneOptimized, hlm, injectCustomClassSettable, injectExposedSideProvider, injectExposesStateProvider, injectTableClassesSettable, provideCustomClassSettable, provideCustomClassSettableExisting, provideExposedSideProvider, provideExposedSideProviderExisting, provideExposesStateProvider, provideExposesStateProviderExisting, provideTableClassesSettable, provideTableClassesSettableExisting, rxHostBinding, rxHostListener, rxHostPressedListener };
//# sourceMappingURL=spartan-ng-ui-core.mjs.map
