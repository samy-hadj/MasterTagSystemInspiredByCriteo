import { Observable } from 'rxjs';
export declare function rxHostListener<T extends Event>(event: string): Observable<T>;
export declare function rxHostBinding<T>(prop: string, stream: Observable<T>): void;
export declare function rxHostPressedListener(): Observable<true | Event>;
