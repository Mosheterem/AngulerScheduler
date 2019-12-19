import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { LoginComponent } from './login/login.component';
import { RouterModule, Routes } from '@angular/router';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { UserService } from '../../Services/user.service';
import { ForgotpasswordComponent } from './forgotpassword/forgotpassword.component';
import { Ng2LoadingSpinnerModule } from 'ng2-loading-spinner';
import { ResetPasswordComponent } from './ResetPassword/reset-password/reset-password.component';
import { ThankyouComponent } from './Feedback/thankyou/thankyou.component';
import { UnsubscribeComponent } from './unsubscribe/unsubscribe.component';
import { NotfoundComponent } from './notfound/notfound.component';

//ThankyouComponentNotfoundComponent
const routes: Routes = [
    { path: '', redirectTo: 'login', pathMatch: 'full' },
    { path: 'login', component: LoginComponent },
    { path: 'forgotpassword', component: ForgotpasswordComponent },
    { path: 'thankyou', component: ThankyouComponent },
    { path: 'unsubscribe', component: UnsubscribeComponent },
  { path: 'resetPassword/:email', component: ResetPasswordComponent },
    { path: '**', component: NotfoundComponent }
]

@NgModule({
    declarations: [
        LoginComponent,
        ForgotpasswordComponent,
        ResetPasswordComponent,
        ThankyouComponent,
    UnsubscribeComponent,
    NotfoundComponent
    ],
    imports: [
        CommonModule,
        RouterModule.forChild(routes),
        ReactiveFormsModule,
        Ng2LoadingSpinnerModule.forRoot({}),
        FormsModule
    ],
    providers: [],

})
export class PublicModule { }
