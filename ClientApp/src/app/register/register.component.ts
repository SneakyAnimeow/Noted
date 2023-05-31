import {Component, Inject} from "@angular/core";
import {HttpClient} from "@angular/common/http";
import {LogInComponent} from "../log-in/log-in.component";

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html'
})
export class RegisterComponent {
  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
  }

  register() {
    const username = (<HTMLInputElement>document.getElementById("username-input")).value;
    const password = (<HTMLInputElement>document.getElementById("password-input")).value;
    const email = (<HTMLInputElement>document.getElementById("email-input")).value;

    if(username === "" || password === "" || email === ""){
      alert("Please fill all fields");
      return;
    }

    if(username.length < 3 || username.length > 32){
      alert("Username must be between 3 and 32 characters");
      return;
    }

    if(password.length < 3){
      alert("Password must be at least 3 characters");
      return;
    }

    if(email.length < 5 || email.length > 128){
      alert("Email must be between 5 and 128 characters");
      return;
    }

    if(!email.includes("@")){
      alert("Email must contain @");
      return;
    }

    this.http.post(this.baseUrl + 'api/Register', {username: username, password: password, email: email}).subscribe(next => {
      },
      error => {
        const response = error as any;
        const status = response.status;
        const value = response?.error?.text;

        if (status === 200 && value !== undefined) {
          (<HTMLInputElement>document.getElementById("username-input")).value = "";
          (<HTMLInputElement>document.getElementById("password-input")).value = "";
          (<HTMLInputElement>document.getElementById("email-input")).value = "";
          window.localStorage.setItem("token", value);
          LogInComponent.isLogged = true;
          window.location.href = "/";
        }else{
          alert("Username or email was already taken");
        }
      });
  }
}
