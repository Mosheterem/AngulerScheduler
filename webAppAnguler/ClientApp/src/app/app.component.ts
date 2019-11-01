import { Component } from '@angular/core';
import "devextreme-intl";
// Dictionaries for German and Russian languages
// import deMessages from "devextreme/localization/messages/he.json";
// import ruMessages from "devextreme/localization/messages/ru.json";
import heMessages from "devextreme/localization/messages/he.json";

import { locale, loadMessages } from "devextreme/localization";
import config from "devextreme/core/config";
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})








export class AppComponent {
    constructor() {
      loadMessages(heMessages);
     // loadMessages(ruMessages);
      locale("he");
      config({ defaultCurrency: "ILS" }); 
     // config({ defaultCurrency: "EUR" });
    }
}
