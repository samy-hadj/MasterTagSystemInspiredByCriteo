import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';

import { AppComponent } from './app.component';
import { HeaderComponent } from './components/header/header.component';
import { TagEditorComponent } from './components/tag-editor/tag-editor.component';
import { TagValidatorComponent } from './components/tag-validator/tag-validator.component';

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    TagEditorComponent,
    TagValidatorComponent,
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
  ],
  providers: [],
  bootstrap: [AppComponent],
  schemas: [CUSTOM_ELEMENTS_SCHEMA], // Ajout de CUSTOM_ELEMENTS_SCHEMA
})
export class AppModule { }
