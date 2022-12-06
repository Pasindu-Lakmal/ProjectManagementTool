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

  addWork(model: any) {
    return this.http.post(this.baseUrl + 'work', model);
  }
  deleteWork(workId: number) {
    return this.http.delete(this.baseUrl + 'work/delete/' + workId, {});
  }

  getWork(workId: number) {
    return this.http.get<Work>(this.baseUrl + 'work/' + workId);
  }
  updateWork(work: Work) {
    return this.http.put(this.baseUrl + 'work', work);
  }
}
