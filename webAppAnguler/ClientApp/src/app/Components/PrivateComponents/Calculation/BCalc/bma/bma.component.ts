import { NgModule, Component, enableProdMode, OnInit } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';
import { HttpClient, HttpClientModule } from '@angular/common/http';

import { DxDataGridModule } from 'devextreme-angular';
import CustomStore from 'devextreme/data/custom_store';

if (!/localhost/.test(document.location.host)) {
  enableProdMode();
}

@Component({
  selector: 'app-bma',
  templateUrl: './bma.component.html',
  styleUrls: ['./bma.component.css']
})

export class BmaComponent implements OnInit {
    ngOnInit(): void {
        //throw new Error("Method not implemented.");
    }

  dataSource: any = {};

  constructor(httpClient: HttpClient) {
    this.dataSource.store = new CustomStore({
      load: function (loadOptions: any) {
        var params = '?';

        params += 'skip=' + loadOptions.skip;
        params += '&take=' + loadOptions.take;

        if (loadOptions.sort) {
          params += '&orderby=' + loadOptions.sort[0].selector;
          if (loadOptions.sort[0].desc) {
            params += ' desc';
          }
        }
        return httpClient.get('https://js.devexpress.com/Demos/WidgetsGallery/data/orderItems' + params)
          .toPromise()
          .then((data: any) => {
            return {
              data: data.items,
              totalCount: data.totalCount
            }
          })
          .catch(error => { throw 'Data Loading Error' });
      }
    });
  }
}



