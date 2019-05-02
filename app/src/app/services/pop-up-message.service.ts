import { Injectable } from '@angular/core';
import { ToastController } from '@ionic/angular';

@Injectable({
  providedIn: 'root'
})
export class PopUpMessageService {



  constructor(private toast: ToastController) { }

  async showInputError(message: string) {
    const toast = await this.toast.create({
      message: message,
      position: "bottom",
      duration: 3000
    });

    toast.present();
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
