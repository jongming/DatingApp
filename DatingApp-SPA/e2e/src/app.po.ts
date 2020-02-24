import { browser, by, element } from 'protractor';

export class AppPage {
  navigateTo() {
    console.log('*****app.po.ts AppPage');
    return browser.get(browser.baseUrl) as Promise<any>;
  }

  getTitleText() {
    console.log('*****app.po.ts getTitleText');
    return element(by.css('app-root .content span')).getText() as Promise<string>;
  }
}
