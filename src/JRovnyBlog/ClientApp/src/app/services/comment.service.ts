import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { InitialComment } from '../models/initial-comment';

@Injectable({
  providedIn: 'root'
})
export class CommentService {
  url = environment.baseUrl;

  constructor(private http: HttpClient) { }

  createInitialComment(id: number, comment: InitialComment) {
    return this.http.post(`${this.url}/api/posts/${id}/comment-initial-anon`, comment);
  }
}
