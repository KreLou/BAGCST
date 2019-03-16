import { Component, OnInit } from '@angular/core';
import {UserSettingsLoaderService} from '../../services/httpServices/user-settings-loader.service';

@Component({
  selector: 'app-settings',
  templateUrl: './settings.page.html',
  styleUrls: ['./settings.page.scss'],
})
export class SettingsPage implements OnInit {

  private userSettings: UserSettings;

  constructor(private settingsLoader: UserSettingsLoaderService) { }

  ngOnInit() {
  }

}
