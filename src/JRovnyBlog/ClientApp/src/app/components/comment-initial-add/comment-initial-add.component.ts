import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, NgForm, Validators } from '@angular/forms';
import { Subscription } from 'rxjs';
import { InitialComment } from 'src/app/models/initial-comment';
import { CommentService } from 'src/app/services/comment.service';

@Component({
  selector: 'app-comment-initial-add',
  templateUrl: './comment-initial-add.component.html',
  styleUrls: ['./comment-initial-add.component.scss']
})
export class CommentInitialAddComponent implements OnInit, OnDestroy {

  form = this.fb.group({
    content: ['', Validators.required],
    email: ['', [Validators.required, Validators.email]],
    name: ['', Validators.required],
    website: [''],
    rememberMe: [false, Validators.required]
  });
  subscription$: Subscription;
  @ViewChild('formDirective') private formDirective: NgForm;

  constructor(private fb: FormBuilder, private commentService: CommentService) { }

  ngOnInit(): void {
  }

  onSubmit() {
    const model = this.form.value;
    const comment: InitialComment = {
      content: model.content,
      email: model.email,
      name: model.name,
      website: model.website,
      rememberMe: model.rememberMe
    }
    this.subscription$ = this.commentService.createInitialComment(11, comment).subscribe(() => {
      this.formDirective.resetForm();
    });
  }

  ngOnDestroy(): void {
    this.subscription$.unsubscribe();
  }
}
