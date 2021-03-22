import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import {MatCardModule} from '@angular/material/card';
import { LoginComponent } from './login/login.component';
import { HeaderComponent } from './header/header.component';
import { MatToolbarModule} from '@angular/material/toolbar';
import {MatButtonModule} from '@angular/material/button';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RegsiterComponent } from './regsiter/regsiter.component';
import { SearchComponent } from './search/search.component';
import { SearchByIsbnComponent } from './search-by-isbn/search-by-isbn.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {MatSnackBarModule} from '@angular/material/snack-bar';
import { AuthenticationService } from './services/authentication/authentication.service';
import { GoogleBookApiService } from './services/bookService/google-book-api.service';
import { FavouriteComponent } from './favourite/favourite.component';
import { MatFormFieldModule} from '@angular/material/form-field';
import {MatIconModule} from '@angular/material/icon';
import {MatSidenavModule} from '@angular/material/sidenav';
import {MatListModule} from '@angular/material/list';
import { FavouriteService } from './services/favouriteService/favourite.service';
import { RouterService } from './services/routerService/router.service';
import { NavBarComponent } from './nav-bar/nav-bar.component';
import { FooterComponent } from './footer/footer.component';
import { RecommendComponent } from './recommend/recommend.component';
import { LogoutComponent } from './logout/logout.component';
import {MatGridList, MatGridListModule, MatGridTile} from '@angular/material/grid-list';

@NgModule({
  declarations: [
    AppComponent,
    DashboardComponent,
    LoginComponent,
    HeaderComponent,
    RegsiterComponent,
    SearchComponent,
    SearchByIsbnComponent,
    FavouriteComponent,
    NavBarComponent,
    FooterComponent,
    RecommendComponent,
    LogoutComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    AppRoutingModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    MatSnackBarModule,
    MatToolbarModule,
    MatButtonModule,
    MatFormFieldModule,
    MatIconModule,
    MatSidenavModule,
    MatCardModule,
    MatListModule,
    MatListModule,
    MatGridListModule
  ],
  providers: [   
    AuthenticationService,
    GoogleBookApiService,
    FavouriteService,
    RouterService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
