import { Injectable } from '@angular/core';
import { ToastController } from '@ionic/angular';

@Injectable({
  providedIn: 'root'
})
export class PopUpMessageService {



  constructor(private toast: ToastController) { }

  async showInputError(message: string) {
    this.toast.create({
      message: message,
      position: "bottom",
      duration: 3000,
      cssClass: 'customToast'
    }).then((obj) => {
      console.log('Toast ', obj);
      obj.present();
    });
  }

  async showAPISuccess(message: string) {
    const toast = await this.toast.create({
      message: message,
      position: 'bottom',
      duration: 2000
    });
    toast.present();
  }
}
