import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { ReactiveFormsModule } from '@angular/forms';
import { JwtModule } from '@auth0/angular-jwt'

import { LoginComponent } from './components/user/login/login.component'
import { RegisterComponent } from './components/user/register/register.component'
import { EditComponent } from './components/user/edit/edit.component';

import { HomeComponent } from './components/home/home.component';
import { NavMenuComponent } from './components/nav-menu/nav-menu.component';
import { FooterComponent } from './components/footer/footer.component'

/* Weather */
import { SearchComponent } from './components/search/search.component'
import { CityComponent } from './components/city/city.component'

import { AppComponent } from './app.component';

import { AppRoutingModule } from './app-routing.module';

/* Angular material */
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AngularMaterialModule } from './angular-material.module';
import { NgModule } from '@angular/core';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';

import {DragDropModule} from '@angular/cdk/drag-drop';
import {NgbPaginationModule, NgbAlertModule} from '@ng-bootstrap/ng-bootstrap';

export function tokenGetter() {
  return localStorage.getItem("jwt");
}

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    LoginComponent,
    RegisterComponent,
    EditComponent,
    SearchComponent,
    CityComponent,
    NavMenuComponent,
    FooterComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    FontAwesomeModule,
    ReactiveFormsModule,
    DragDropModule,
    BrowserModule,
    BrowserAnimationsModule,
    NgbPaginationModule, 
    NgbAlertModule,
    AngularMaterialModule,
    JwtModule.forRoot({
      config: {
        tokenGetter: tokenGetter,
        allowedDomains: ["localhost:44322"],
        disallowedRoutes: []
      }
    }),
    BrowserAnimationsModule,
    FontAwesomeModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
