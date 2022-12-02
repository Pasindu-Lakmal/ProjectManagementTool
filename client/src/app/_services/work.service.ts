import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Work } from '../_models/work';

@Injectable({
  providedIn: 'root',
})
export class WorkService {
  work: Work[] = [];
  baseUrl = environment.apiUrl;
  constructor(private http: HttpClient) {}

  getWorks() {
    return this.http.get<Work[]>(this.baseUrl + 'work');
  }
}
