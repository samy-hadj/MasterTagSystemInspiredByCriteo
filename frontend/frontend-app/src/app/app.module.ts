import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { IntegralUIModule } from '@lidorsystems/integralui-web/bin/integralui/integralui.module';

import { AppComponent } from './app.component';
import { HeaderComponent } from './components/header/header.component';
import { TagEditorComponent } from './components/tag-editor/tag-editor.component';
import { TagValidatorComponent } from './components/tag-validator/tag-validator.component';
import { TreeViewComponent } from './tree-view/tree-view.component';

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    TagEditorComponent,
    TagValidatorComponent,
    TreeViewComponent,
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    IntegralUIModule, // Ajout de IntegralUIModule ici
  ],
  providers: [],
  bootstrap: [AppComponent],
  schemas: [CUSTOM_ELEMENTS_SCHEMA], // Ajout de CUSTOM_ELEMENTS_SCHEMA
})
export class AppModule { }
