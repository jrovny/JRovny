import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { InitialComment } from 'src/app/models/initial-comment';
import { CommentService } from 'src/app/services/comment.service';

@Component({
  selector: 'app-comment-initial-add',
  templateUrl: './comment-initial-add.component.html',
  styleUrls: ['./comment-initial-add.component.scss']
})
export class CommentInitialAddComponent implements OnInit {

  form = this.fb.group({
    content: ['', Validators.required],
    email: ['', [Validators.required, Validators.email]],
    name: ['', Validators.required],
    website: ['']
  })

  constructor(private fb: FormBuilder, private commentService: CommentService) { }

  ngOnInit(): void {
  }

  onSubmit() {
    const model = this.form.value;
    const comment: InitialComment = {
      content: model.content,
      email: model.email,
      name: model.name,
      website: model.website
    }
    this.commentService.createInitialComment(11, comment).subscribe(comment => console.log('Saving comment', comment));
  }
}
