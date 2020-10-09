import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { PostDetail } from 'src/app/models/post-detail';
import { AppService } from 'src/app/services/app.service';

@Component({
  selector: 'app-post-detail',
  templateUrl: './post-detail.component.html',
  styleUrls: ['./post-detail.component.scss']
})
export class PostDetailComponent implements OnInit {
  slug: any;
  post$: Observable<PostDetail>;

  constructor(private route: ActivatedRoute, private appService: AppService) { }

  ngOnInit(): void {
    this.route.params.subscribe(slug => { this.slug = slug.slug; console.log(slug) });
    this.post$ = this.appService.getBlogPostBySlug(this.slug);
  }

}
