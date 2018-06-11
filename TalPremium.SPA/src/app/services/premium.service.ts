import { environment } from './../../environments/environment';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class PremiumService {
  private url = environment.apiUrlPremium;

  constructor(private http: HttpClient) { }

  calculatePremium(userData) {
    return this.http.post(this.url, userData);
  }

}
