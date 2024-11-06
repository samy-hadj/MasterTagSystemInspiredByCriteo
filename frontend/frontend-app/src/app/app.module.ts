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
import { TreeDataService } from './services/tree-data.service';  // Assurez-vous de l'importer ici

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    TagEditorComponent,
    TreeViewComponent,
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    BrowserAnimationsModule,
    MatTreeModule,
    MatIconModule,
    MatButtonModule,
  ],
  providers: [TreeDataService],  // Assurez-vous d'ajouter ce service ici
  bootstrap: [AppComponent],
})
export class AppModule { }
