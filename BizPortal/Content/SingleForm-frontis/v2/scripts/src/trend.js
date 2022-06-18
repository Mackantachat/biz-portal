import $ from "jquery";
import _ from 'lodash';
import swal from 'sweetalert2';
import { showLoading, hideLoading, getTrends, GetTrendsByRootTaxonomyId } from './api';

 let page = '';
 let site = '';
 

 checkPage();

function checkPage() {
  const link = window.location.href;
  const link2 = window.location.pathname;
  if (_.size($(".trend")) > 0) {
    if (link.indexOf('business.html') !== -1) {
      page = '1';
      site = 'business-info';
    } else if (link.indexOf('citizen.html') !== -1) {
      page = '2';
      site = 'citizen-info';
    } else  {
      page = '0';
      site = 'taxonomy-info';
    }
  }
};

if (!_.isEmpty(page)) {
  getInit();

  async function getInit() {
    showLoading();
    try{
      let trends = [];
      if (page !== '0') {
        trends = await GetTrendsByRootTaxonomyId(page);
      } else {
        trends = await getTrends();
      }

      const trendsItems = _.map(trends, data => {
        return `
          <a href="htpp://www.info.go.th/${site}.html?ProcedureID=${_.get(data, 'ProcedureID', '')}" class="trend-item">
            <p>${_.get(data, 'ProcedureName', '')}</p>
          </a>
        `
      });
      $('#most-trend').html(trendsItems); 
    } catch (e) {

    }
    hideLoading();
  };

}
