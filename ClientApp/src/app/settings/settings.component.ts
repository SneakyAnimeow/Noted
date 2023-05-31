import {Component, Inject} from "@angular/core";
import {HttpClient} from "@angular/common/http";
import {LogInComponent} from "../log-in/log-in.component";
import {UserDto} from "../api-dtos";

@Component({
  selector: 'app-settings',
  templateUrl: './settings.component.html'
})
export class SettingsComponent {
  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
  }

  save() {
    if (!confirm("Are you sure you want to save these changes?")) {
      return;
    }

    const token = <string> window.localStorage.getItem("token");
    const username = (<HTMLInputElement>document.getElementById("username-input")).value;
    const email = (<HTMLInputElement>document.getElementById("email-input")).value;
    let password: string | null = <string>(<HTMLInputElement>document.getElementById("password-input")).value;

    if (username.length < 3 || username.length > 32) {
      alert("Username must be between 3 and 32 characters");
      return;
    }

    if (email.length < 5 || email.length > 128) {
      alert("Email must be between 5 and 128 characters");
      return;
    }

    if (!email.includes("@")) {
      alert("Email must contain @");
      return;
    }

    if (password === "") {
      password = null;
    } else {
      if (password.length < 3) {
        alert("Password must be at least 3 characters");
        return;
      }
    }

    this.http.post(this.baseUrl + 'api/User/UpdateUser', { token: token, username: username, email: email, password: password })
      .subscribe(next => {
        const user = next as UserDto;
        LogInComponent.user = user;
        alert("Saved");
      }, error => {
        const response = error as any;
        const status = response.status;
        const value = response?.error?.text;

        //TODO: Add proper error handling
        alert(value);
      });
  }

  ngDoCheck() {
    if (LogInComponent.isLogged) {
      this.setValues();
    }
  }

  setValues() {
    (<HTMLInputElement>document.getElementById("username-input")).value = <string>LogInComponent.user?.username;
    (<HTMLInputElement>document.getElementById("email-input")).value = <string>LogInComponent.user?.email;
  }

  protected readonly LogInComponent = LogInComponent;
}
