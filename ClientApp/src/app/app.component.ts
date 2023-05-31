import {Component, Inject} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {LogInComponent} from "./log-in/log-in.component";
import {UserDto} from "./api-dtos";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent {
  title = 'app';

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
  }

  ngOnInit() {
    const token = window.localStorage.getItem("token");
    if (token !== null) {
      this.http.post(this.baseUrl + 'api/TokenStillValid', {token: token}).subscribe(next => {
        if (next !== true) {
          window.localStorage.removeItem("token");
          LogInComponent.isLogged = false;
          return;
        }

        LogInComponent.isLogged = true;

        this.http.post(this.baseUrl + 'api/User/GetInfo', {token: token}).subscribe(next => {
          LogInComponent.user = next as UserDto;
        }, error => {
        });
      }, error => {
      });
    }
  }
}
