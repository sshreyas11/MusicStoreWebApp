export class User{
    user_type: string;
    first_name: string;
    last_name: string;
    email: string;
    password: string;
    usr_pho: string;
    constructor(){
        this.email = '';
        this.user_type = '';
        this.first_name = '';
        this.last_name = '';
        this.password = '';
        this.usr_pho = '';
    }
}