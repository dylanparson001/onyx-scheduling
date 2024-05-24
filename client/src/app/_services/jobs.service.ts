import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {environment} from "../environments/environment";
import {Observable} from "rxjs";
import {Jobs} from "../models/jobs";
import {Invoice} from "../models/invoice";
import {Item} from "../models/item";

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

  postItems(itemIds: Item[], jobId: number) {
    return this.http.post(
      `${this.baseUrl}Jobs/AddItemsToJob?jobId=${jobId}`,
      itemIds
    )
  }

  getJobsByTechAndDate(date: string, techId: string) {
    return this.http.get<Jobs[]>(
      `${this.baseUrl}Jobs/GetJobsByTechnician?date=${date}&technicianId=${techId}`
    )
  }

  getItemsFromJob(jobId: number) {
    return this.http.get<Item[]>(
      `${this.baseUrl}Jobs/GetItemsFromJob?jobId=${jobId}`
    )
  }
  removeItemsFromJob(jobId: number, itemToDelete:number) {
    return this.http.delete(
      `${this.baseUrl}Jobs/RemoveItemsFromJob?jobId=${jobId}&itemToDelete=${itemToDelete}`
    )
  }

  changeProcessingStatus(jobId: number, newStatus: string) {
    console.log(newStatus)
    return this.http.put(
      `${this.baseUrl}Jobs/UpdateJobStatus?jobId=${jobId}&newStatus=${newStatus}`,
      newStatus
    )
  }

}
