import { Component, OnInit, TemplateRef } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Areas } from 'src/app/core/models/general/Areas.model';
import { LoggedInUser } from 'src/app/core/models/user/loggedin-user';
import { GeneralService } from 'src/app/core/service/general.service';
import { UserService } from 'src/app/core/service/user.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-myposts',
  templateUrl: './myposts.component.html',
  styleUrls: ['./myposts.component.scss']
})
export class MyPostsComponent implements OnInit {

  user: LoggedInUser;
  areas: Areas[] = [
    { id: 0, name: 'All' }
  ];

  post: {
    postId: number,
    payPerHour: number,
    role: string,
    desc: string,
    areaCode: number,
  } = {
      postId: 0,
      role: '',
      desc: '',
      areaCode: 0,
      payPerHour: 0
    }
  messageList: any[] = [];

  constructor(private userService: UserService, private modalService: NgbModal, private generalService: GeneralService) { };

  ngOnInit() {
    this.userService.getUserSubject().subscribe(e => {
      this.user = e;
    });
    this.generalService.getAreas().subscribe((data: Areas[]) => {
      this.areas = data;
      this.areas.unshift({ id: 0, name: 'All' });
    });
    this.generalService.getMyPosts().subscribe((data: any) => {
      this.messageList = data;
    });
  }

  openModal(content: TemplateRef<any>) {
    this.post.postId = 0;
    this.post.areaCode = 0;
    this.post.payPerHour = 0;
    this.post.desc = '';
    this.post.role = '';

    this.modalService.open(content, { centered: true }).result.then((result) => {
    }).catch((res) => { });
  }

  openEditModal(content: TemplateRef<any>, data: any) {
    this.post.postId = data.id;
    this.post.areaCode = data.areaId;
    this.post.payPerHour = data.payPerHour;
    this.post.desc = data.desc;
    this.post.role = data.role;

    this.modalService.open(content, { centered: true }).result.then((result) => {
    }).catch((res) => { });
  }

  createPost() {
    this.generalService.createPost({ ...this.post, isMyPosts: true }).subscribe((data: any) => {
      this.messageList = data;

      const Toast = Swal.mixin({
        toast: true,
        position: 'top-end',
        showConfirmButton: false,
        timer: 2000,
        timerProgressBar: true,
      })

      Toast.fire({
        icon: 'success',
        title: 'Success'
      })

    });
    this.post.postId = 0;
    this.post.areaCode = 0;
    this.post.payPerHour = 0;
    this.post.desc = '';
    this.post.role = '';
  }

  getProfile(url: string) {
    return this.userService.getProfileUrl(url);
  }

  requestForMessage() {
    this.generalService.getMyPosts().subscribe((data: any) => {
      this.messageList = data;
    });
  }

}
