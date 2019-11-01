import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { PublicLayoutComponent } from './Containers/Layout/public-layout/public-layout.component';
import { PrivateLayoutComponent } from './Containers/Layout/private-layout/private-layout.component';
import { PublicModule } from './Components/PublicComponents/Public.module';
import { PrivateModule } from './Components/PrivateComponents/Private.module';
import { AuthGuardService } from './Helpers/auth-guard.service';

const route: Routes =
  [

    {
      path: 'public',
      component: PublicLayoutComponent,
      children: [
        {
          path: '',
          loadChildren: () => PublicModule
        }

      ]

    },
    {
      path: 'private',
      component: PrivateLayoutComponent,
      children: [
        {
          path: '',
          loadChildren: () => PrivateModule


        }
      ]
    },

    {
      path: '',
      redirectTo: 'public',
      pathMatch: 'full'
    }

  ]

@NgModule({
  imports: [
    RouterModule.forRoot(route)
  ],
  declarations: [],

  exports: [RouterModule]
})
export class AppRoutingModule { }
