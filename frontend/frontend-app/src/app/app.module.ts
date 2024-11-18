import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { AppComponent } from './app.component';
import { HeaderComponent } from './components/header/header.component';
import { TagEditorComponent } from './components/tag-editor/tag-editor.component';
import { TreeViewComponent } from './components/tree-view/tree-view.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatTreeModule } from '@angular/material/tree';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { InfoTableComponent } from './components/info-table/info-table.component';
import { MatTooltipModule } from '@angular/material/tooltip';
import { SearchBarComponent } from './components/search-bar/search-bar.component';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';  // Pour les champs de texte
import { MatListModule } from '@angular/material/list';    // Pour la liste des éléments


@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    TagEditorComponent,
    TreeViewComponent,
    InfoTableComponent,
    SearchBarComponent,
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    BrowserAnimationsModule,
    MatTreeModule,
    MatIconModule,
    MatButtonModule,
    BrowserModule,
    MatTooltipModule,
    MatFormFieldModule,
    MatInputModule,
    MatListModule,
  ],
  providers: [],  // Assurez-vous d'ajouter ce service ici
  bootstrap: [AppComponent],
})
export class AppModule { }
