import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { debug } from 'util';
import { CommonAlertService } from '../../../Helpers/common-alert.service';
import { feedBackService } from '../../../Services/feedBack.service';
import { Status } from '../../../Models/response-model';

@Component({
    selector: 'app-unsubscribe',
    templateUrl: './unsubscribe.component.html',
    styleUrls: ['./unsubscribe.component.css']
})
export class UnsubscribeComponent implements OnInit {
    ID: string;
    constructor(private route: ActivatedRoute, private alert: CommonAlertService, private _feedBackService: feedBackService, )
    { this.route.queryParams.subscribe(params => { this.ID = params['id']; }); }

    ngOnInit() {
    }

    unsubscribe() {

        this._feedBackService.Unsubscribe(this.ID).subscribe(resp => {
            if (resp.status == Status.Success) {
              this.alert.ShowSuccessAlert('הודעתך נרשמה בהצלחה');
            }
            else {
                this.alert.ShowErrorAlert(resp.message);
            }
        });
    }
}
