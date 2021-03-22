import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { AuthenticationGuard } from './authentication.guard';
import { DashboardComponent } from './dashboard/dashboard.component';
import { FavouriteComponent } from './favourite/favourite.component';
import { LoginComponent } from './login/login.component';
import { LogoutComponent } from './logout/logout.component';
import { RecommendComponent } from './recommend/recommend.component';
import { RegsiterComponent } from './regsiter/regsiter.component';
import { SearchComponent } from './search/search.component';

const routes: Routes = [
 { path:'',
  children:[
    
   { path:'', redirectTo: '/login', pathMatch:'full' },
   { path:'register',component:RegsiterComponent },
   { path:'login', component:LoginComponent },
   { path:'logout', component:LogoutComponent},
   {path:'recommendedbook',component:RecommendComponent},
   {path:'dashboard', component: DashboardComponent}
    
]},
{path:'fav-list', component:FavouriteComponent},
{path:'book-search', component:SearchComponent},
{ path: 'default', component: AppComponent },

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { };
