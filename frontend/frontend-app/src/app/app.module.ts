// app.module.ts
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';

// Material Modules
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatListModule } from '@angular/material/list';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatTreeModule } from '@angular/material/tree';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';

// Components
import { AppComponent } from './app.component';
import { HeaderComponent } from './components/header/header.component';
import { InfoTableComponent } from './components/info-table/info-table.component';
import { SearchBarComponent } from './components/search-bar/search-bar.component';
import { TagEditorComponent } from './components/tag-editor/tag-editor.component';
import { TreeViewComponent } from './components/tree-view/tree-view.component';


import { HlmButtonDirective } from '@spartan-ng/ui-button-helm';

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    InfoTableComponent,
    SearchBarComponent,
    TagEditorComponent,
    TreeViewComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    FormsModule,
    HttpClientModule,
    CommonModule,
    MatFormFieldModule,
    MatInputModule,
    MatListModule,
    MatTooltipModule,
    MatTreeModule,
    MatIconModule,
    MatButtonModule,
    HlmButtonDirective
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }