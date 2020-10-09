import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { PostDetail } from 'src/app/models/post-detail';
import { AppService } from 'src/app/services/app.service';

@Component({
  selector: 'app-post-detail',
  templateUrl: './post-detail.component.html',
  styleUrls: ['./post-detail.component.scss']
})
export class PostDetailComponent implements OnInit {

  constructor(private appService: AppService) { }

  post$: Observable<PostDetail>;

  ngOnInit(): void {

    this.post$ = this.appService.getBlogPostBySlug('test');
  }

}
