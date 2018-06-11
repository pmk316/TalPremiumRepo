import { PremiumData } from './../../models/premium';
import { Component, OnInit } from '@angular/core';
import { PremiumService } from './../../services/premium.service';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-premium',
  templateUrl: './premium.component.html',
  styleUrls: ['./premium.component.css']
})
export class PremiumComponent implements OnInit {
  userData;
  premiumData;

  constructor(private premiumService: PremiumService) { }

  ngOnInit() {
    this.initializeUserData();
    this.initializePremiumData();
  }

  //Routine to initialize the user input data
  initializeUserData() {
    this.userData = {
      name: "",
      birthDate: "",
      isMale: "",        
    };
  }

  //Routine to initialize the premium output data
  initializePremiumData() {
    if (this.premiumData) {
      this.premiumData.premiumAmount = null;
      this.premiumData.errorString = "";
    }
  }  

  //Routine to fetch premium from backend REST API
  onSubmit() {
    console.log("submit", this.userData);
    this.premiumService.calculatePremium(this.userData)
      .subscribe((premiumData : PremiumData) => {
        this.premiumData = premiumData;
        console.log("premium=", this.premiumData);
      });
  }

  //Routine to clear the form
  onClear(form: NgForm) {
    form.resetForm();
    this.initializeUserData();
    this.initializePremiumData();
  }
}
