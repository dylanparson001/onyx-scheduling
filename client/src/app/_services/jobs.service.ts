import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {environment} from "../environments/environment";
import {Observable} from "rxjs";
import {Jobs} from "../models/jobs";
import {Invoice} from "../models/invoice";

@Injectable({
  providedIn: 'root'
})
export class JobsService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getJobsByTechnicianAndDate(date: string, status: string, position: number, take: number):Observable<Jobs[]> {
    return this.http.get<Jobs[]>(
      `${this.baseUrl}Jobs/GetJobsByDateAndStatus?${date}&${status}&${position}&${take}`
    )
  }

  getJobStatusList() {
    return this.http.get<string[]>(
      `${this.baseUrl}Jobs/GetJobStatusList`
    )
  }

  postJob(newJob: Jobs): Observable<Invoice> {
    return this.http.post<Invoice>(
      `${this.baseUrl}Jobs/AddJob`,
      newJob
    );
  }

}
