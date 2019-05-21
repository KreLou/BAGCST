import { Injectable } from '@angular/core';
import { DeviceInformation } from './DeviceInformation';

@Injectable({
  providedIn: 'root'
})
export class DeviceInformationLoaderService {

  private info: DeviceInformation;
  constructor() { }


  getDeviceInformation(): DeviceInformation {
    return this.info;
  }
}
