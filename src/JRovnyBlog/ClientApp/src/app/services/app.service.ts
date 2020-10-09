import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { PostSummary } from "../models/post-summary";
import { environment } from 'src/environments/environment';
import { PostDetail } from '../models/post-detail';

@Injectable({
  providedIn: "root",
})
export class AppService {
  url = environment.baseApiUrl;

  constructor(private http: HttpClient) { }

  getBlogPostSummaries() {
    return this.http.get<PostSummary[]>(`${this.url}/posts`);
  }

  getBlogPostBySlug(slug: string) {
    return this.http.get<PostDetail>(`${this.url}/posts/slug/${slug}`)
  }
}
