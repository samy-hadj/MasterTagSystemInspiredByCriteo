import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http'; // Assurez-vous que c'est importé

import { AppComponent } from './app.component';
import { HeaderComponent } from './components/header/header.component';
import { HomeComponent } from './components/home/home.component';
import { TagValidatorComponent } from './components/tag-validator/tag-validator.component';
import { FormsModule } from '@angular/forms'; // Importation de FormsModule

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    HomeComponent,
    TagValidatorComponent,
    
  ],
  imports: [
    FormsModule, // Ajout de FormsModule ici
    BrowserModule,
    HttpClientModule // Assurez-vous d'ajouter ce module ici
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
