export class User {
    primeID: number;
    id: number;
    username: string;
    password: string;
    //firstName: string;
    //lastName: string;
    clientID:string;
    eMail:string;
    resourceName:string;
    connectionString:string;
    lastUserLogin:Date;
    lastDBUpdated:Date
    macAdress:string;
    cpuid:string;
    constructor(data?:any){
        data && data.primeID? this.primeID= data.primeID : null;
        data && data.id? this.id= data.id : null;
        data && data.username? this.username= data.username : null;
        data && data.password? this.password= data.password : null;
        data && data.clientID? this.clientID= data.clientID : null;
        data && data.eMail? this.eMail= data.eMail : null;
        data && data.resourceName? this.resourceName= data.resourceName : null;
        data && data.connectionString? this.connectionString= data.connectionString : null;
        data && data.lastUserLogin? this.lastUserLogin= data.lastUserLogin : null;
        data && data.lastDBUpdated? this.lastDBUpdated= data.lastDBUpdated : null;
        data && data.macAdress? this.macAdress= data.macAdress : null;
        data && data.cpuid? this.cpuid= data.cpuid : null;

    }

}


