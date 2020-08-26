import { Injectable, Inject } from "@angular/core";
import { HttpClient, HttpResponse } from "@angular/common/http";
import { HttpClientModule } from '@angular/common/http';
import { Observable, Subscriber } from 'rxjs';
import { Child } from "./child";


@Injectable()
export class AppService {

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  getChildren(): Observable<Child[]> {
    return this.http.get<Child[]>(this.baseUrl + 'api/Data');
  }

  addChild(child: Child): Observable<Child> {
    return this.http.post<Child>(this.baseUrl + 'api/Data/', child);
  }

  deleteChild(Id: number): Observable<{}> {
    return this.http.delete(this.baseUrl + `api/Data/${Id}`);
  }
}
