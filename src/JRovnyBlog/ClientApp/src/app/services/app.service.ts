import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { PostSummary } from "../models/post-summary";

@Injectable({
  providedIn: "root",
})
export class AppService {
  url = "https://localhost:5001/api";

  constructor(private http: HttpClient) {}

  getBlogPostSummaries() {
    return this.http.get<PostSummary[]>(`${this.url}/posts`);
  }
}
