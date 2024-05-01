import { Component, OnInit } from '@angular/core';
import { Areas } from 'src/app/core/models/general/Areas.model';
import { LoggedInUser } from 'src/app/core/models/user/loggedin-user';
import { GeneralService } from 'src/app/core/service/general.service';
import { UserService } from 'src/app/core/service/user.service';

@Component({
  selector: 'app-allposts',
  templateUrl: './general.component.html',
  styleUrls: ['./general.component.scss']
})
export class GeneralComponent implements OnInit {

  user: LoggedInUser;
  areas: Areas[] = [
    { id: 0, name: 'All' }
  ];
  areaId: number | null = 0;
  messageList: any[] = [];

  constructor(private userService: UserService, private generalService: GeneralService) { };

  ngOnInit() {
    this.userService.getUserSubject().subscribe(e => {
      this.user = e;
    });
    this.generalService.getAreas().subscribe((data: Areas[]) => {
      this.areas = data;
      this.areas.unshift({ id: 0, name: 'All' });
    });
    this.generalService.getAllMessages(0).subscribe((data: any) => {
      this.messageList = data;
    });
  }

  getProfile(url: string) {
    return this.userService.getProfileUrl(url);
  }

  requestForMessage() {
    this.generalService.getAllMessages(this.areaId ?? 0).subscribe((data: any) => {
      this.messageList = data;
    });
  }

}
