export class ResponseModel {

    data :  any;
    message : string;
    status: Status
}

export enum Status{
    Error,
    Success,
    Warning
   }
