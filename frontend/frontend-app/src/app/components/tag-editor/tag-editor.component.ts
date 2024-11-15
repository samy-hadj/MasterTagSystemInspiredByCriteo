import { Component, Input, Output, EventEmitter, AfterViewInit, ViewChild, ElementRef, OnChanges, SimpleChanges } from '@angular/core';
import { TagService } from '../../services/tag.service';
import { TagModel } from '../../models/tagModel.model';
import * as ace from 'ace-builds';

@Component({
  selector: 'app-tag-editor',
  templateUrl: './tag-editor.component.html',
  styleUrls: ['./tag-editor.component.css']
})
export class TagEditorComponent implements AfterViewInit, OnChanges {
  @Input() jsonContent: string = '';
  @Input() tagId: string = '';
  @Output() jsonChange = new EventEmitter<string>();
  @Output() updateSuccess = new EventEmitter<void>();

  @ViewChild('editor') editorRef!: ElementRef;
  editor!: ace.Ace.Editor;

  showConfirmation: boolean = false;
  showError: boolean = false;

  private isInternalChange: boolean = false;

  constructor(private tagService: TagService) {}

  ngAfterViewInit() {
    this.editor = ace.edit(this.editorRef.nativeElement);
    this.editor.setTheme('ace/theme/github');
    this.editor.session.setMode('ace/mode/json');
    this.editor.setValue(this.jsonContent, -1);
    this.editor.on('change', () => {
      if (!this.isInternalChange) {
        this.jsonContent = this.editor.getValue();
        this.jsonChange.emit(this.jsonContent);
      }
    });
  }

  ngOnChanges(changes: SimpleChanges) {
    if (changes['jsonContent'] && !changes['jsonContent'].firstChange) {
      const newContent = changes['jsonContent'].currentValue;
      if (this.editor) {
        const cursorPosition = this.editor.getCursorPosition();
        this.isInternalChange = true;
        this.editor.setValue(newContent, -1);
        this.editor.moveCursorToPosition(cursorPosition);
        this.isInternalChange = false;
      }
    }
  }

  saveChanges() {
    try {
      const updatedTag: TagModel = JSON.parse(this.jsonContent);
      this.tagService.updateTag(this.tagId, updatedTag).subscribe(
        (response) => {
          console.log('Tag mis à jour:', response);
          this.showConfirmation = true;
          this.showError = false;
          this.updateSuccess.emit();
          setTimeout(() => this.showConfirmation = false, 2000);
        },
        (error) => {
          console.error('Erreur de mise à jour:', error);
          this.showError = true;
          this.showConfirmation = false;
          setTimeout(() => this.showError = false, 2000);
        }
      );
    } catch (e) {
      console.error('Invalid JSON:', e);
      this.showError = true;
      this.showConfirmation = false;
      setTimeout(() => this.showError = false, 2000);
    }
  }
}