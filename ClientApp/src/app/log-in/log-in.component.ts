import {Component, Inject} from "@angular/core";
import {HttpClient} from "@angular/common/http";
import {UserDto} from "../api-dtos";

@Component({
  selector: 'app-log-in',
  templateUrl: './log-in.component.html'
})
export class LogInComponent {
  public static isLogged: boolean = false;
  public static user: UserDto|null = null;

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
  }

  login() {
    const username = (<HTMLInputElement>document.getElementById("username-input")).value;
    const password = (<HTMLInputElement>document.getElementById("password-input")).value;

    if(username === "" || password === ""){
      alert("Please fill all fields");
      return;
    }

    this.http.post(this.baseUrl + 'api/Login', {username: username, password: password}).subscribe(next => {
      },
      error => {
        const response = error as any;
        const status = response.status;
        const value = response?.error?.text;

        if (status === 200 && value !== undefined) {
          (<HTMLInputElement>document.getElementById("username-input")).value = "";
          (<HTMLInputElement>document.getElementById("password-input")).value = "";
          window.localStorage.setItem("token", value);
          LogInComponent.isLogged = true;
          window.location.href = "/";
        }else{
          alert("Wrong username or password");
        }
      });
  }
}
