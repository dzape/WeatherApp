import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { ReactiveFormsModule } from '@angular/forms';
import { JwtModule } from '@auth0/angular-jwt'

import { LoginComponent } from './components/user/login/login.component'
import { RegisterComponent } from './components/user/register/register.component'

import { HomeComponent } from './components/home/home.component';
import { NavMenuComponent } from './components/nav-menu/nav-menu.component';
import { FooterComponent } from './components/footer/footer.component'

import { WeatherComponent } from './components/weather/weather.component'
import { CityComponent } from './components/city/city.component'

import { AppComponent } from './app.component';

import { AuthGuard } from './services/guards/auth-guard.service';

export function tokenGetter() {
  return localStorage.getItem("jwt");
}

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    LoginComponent,
    RegisterComponent,
    WeatherComponent,
    CityComponent,
    NavMenuComponent,
    FooterComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'login', component: LoginComponent },
      { path: 'register', component: RegisterComponent},
      { path: 'weather', component: WeatherComponent, canActivate: [AuthGuard] },
      { path: 'favourite', component: CityComponent, canActivate: [AuthGuard] },
    ]),
    JwtModule.forRoot({
      config: {
        tokenGetter: tokenGetter,
        whitelistedDomains: ["localhost:44316"],
        blacklistedRoutes: []
      }
    }),
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
