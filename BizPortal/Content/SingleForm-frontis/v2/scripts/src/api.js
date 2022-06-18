import $ from "jquery";
import queryString from 'query-string';
const proxy =  ''; //'localhost:1337/';
const url = `http://${proxy}164.115.32.195/infongback`;
const optionGet = {
  method: 'GET',
  headers: new Headers(),
  credentials: 'same-origin',
  cache: 'no-store',
};
const optionPost = {
  method: 'POST',
  headers: new Headers({'content-type': 'application/json'}),
  credentials: 'same-origin',
  cache: 'no-store',
};
//console.log('FFFF:',proxy);

// export const convertToURLParam = (data) => {
//   return `?${_.join(_.map(data, (value, key) => `${key}=${encodeURIComponent(_.toString(value))}`), '&')}`;
// };

export const convertToParam = (param) => {
  return queryString.stringify(param);
};

export const setURLParameter = (param) => {
  const stringified = queryString.stringify(param);
  location.search = stringified;
};

export const getURLParameter = () => {
  return queryString.parse(location.search);
};

export const showLoading = () => {
  $('.mask, .loading').addClass('active');
};

export const hideLoading = () => {
  $('.mask, .loading').removeClass('active');
};

export const getTrends = () => {
  return fetchApi(`${url}/Api/Trends/a/GetTrends`, optionGet)
  .then((response) => {
      return response;
  });
};

export const GetTrendsByRootTaxonomyId = (id) => {
  return fetchApi(`${url}/Api/Trends/a/GetTrendsByRootTaxonomyId/${id}`, optionGet)
  .then((response) => {
      return response;
  });
};

export const  fetchApi = (url, options) => {
 return fetch(url, options)
  .then(response => response.json().then(json => ({ json, response })))
  .then(({ json, response }) => {
    return json
  })
};

export const GetProcedureDetailV2 = (id) => { ///default
  return fetchApi(`${url}/Api/Procedure/a/GetProcedureDetailV2/${id}`, optionGet)
  .then((response) => {
      return response;
  });
};

export const GetCitizenGuideDetail = (id, organizecode) => {   ///1
  return fetchApi(`${url}/Api/CitizenGuide/a/GetCitizenGuideDetail/${id}?orgCode=${organizecode}`, optionGet)
  .then((response) => {
      return response;
  });
};

export const GetCitizenGuideBoundariesByCitizenGuideID = (id) => { ////2
  return fetchApi(`${url}/Api/CitizenGuide/a/GetCitizenGuideBoundariesByCitizenGuideID/${id}`, optionGet)
  then((response) => {
      return response;
  });
};

export const GetCitizenGuideStepsByCitizenGuideID = (id) => { ///3
  return fetchApi(`${url}/Api/CitizenGuide/a/GetCitizenGuideStepsByCitizenGuideID/${id}`, optionGet)
  then((response) => {
      return response;
  });
};

export const GetCitizenGuideDocumentsByCitizenGuideID = (id) => { ///4
  return fetchApi(`${url}/Api/CitizenGuide/a/GetCitizenGuideDocumentsByCitizenGuideID/${id}`, optionGet)
  then((response) => {
      return response;
  });
}

export const GetCitizenGuideFeesByCitizenGuideID = (id) => { ///5
  return fetchApi(`${url}/Api/CitizenGuide/a/GetCitizenGuideFeesByCitizenGuideID/${id}`, optionGet)
  then((response) => {
      return response;
  });
};

export const GetCitizenGuideComplainsByCitizenGuideID = (id) => { ///6
  return fetchApi(`${url}/Api/CitizenGuide/a/GetCitizenGuideComplainsByCitizenGuideID/${id}`, optionGet)
  then((response) => {
      return response;
  });
};

export const GetCitizenGuideExampleDocumentsByCitizenGuideID = (id) => { ///7
  return fetchApi(`${url}/Api/CitizenGuide/a/GetCitizenGuideExampleDocumentsByCitizenGuideID/${id}`, optionGet)
  then((response) => {
      return response;
  });
};

export const GetRelationProcedureByProcedureID = (id)=> {
  return fetchApi(`${url}/Api/Procedure/a/GetRelationProcedureByProcedureID/${id}`, optionGet)
  then((response) => {
      return response;
  });
};

export const GenerateCitizenGuideFront = (id) => { ///8
  return fetchApi(`${url}/Guide/GenerateCitizenGuideFront/${id}`, optionGet)
  then((response) => {
      return response;
  });
};

export const GetCitizenGuideCheckList = (id) => { ///9
  return fetchApi(`${url}/Guide/GenerateCitizenGuideCheckList/${id}`, optionGet)
  then((response) => {
      return response;
  });
};


export const Ministries = () => {
  return fetchApi(`${url}/Api/Organization/a/Ministries`, optionGet)
  .then((response) => {
      return response;
  });
};

export const getDepartmentByMinistries = (code) => {
  return fetchApi(`${url}/Api/Organization/a/Departments?MinistryCode=${code}`, optionGet)
  .then((response) => {
      return response;
  });
};

export const RequestSendDocToMail = (list, id, guideId) => {
  const data = {
    Receiver: list,
    ProcedureID: id,
    CitizenGuideID: guideId,
  };

  return fetchApi(`${url}/Api/CitizenGuide/a/RequestSendDocToMail`, {
    ...optionPost,
    body: JSON.stringify(data)
  })
  .then((response) => {
      return response;
  });
};

export const SendQuestion = (question, name, citizenId, tel, message, email) => {
  const data = {
    Question: question,
    Name: name,
    CitizenId: citizenId,
    Telephone:tel ,
    Message: message,
    Email: email,
  };

  return fetchApi(`${url}/Api/Contact/a/SendQuestion`, {
    ...optionPost,
    body: JSON.stringify(data)
  })
  .then((response) => {
      return response;
  });
};

export const GetTaxonomies = (id) => {
  return fetchApi(`${url}/Api/Taxonomy/a/GetTaxonomies/${id}`, optionGet)
  .then((response) => {
      return response;
  });
};

export const GetRootParentTaxonomies = () => {
  return fetchApi(`${url}/Api/Taxonomy/a/GetRootParentTaxonomies`, optionGet)
  .then((response) => {
      return response;
  });
};

export const GetTaxonomyByTaxonomyID = (id) => {
  return fetchApi(`${url}/Api/Taxonomy/a/GetTaxonomyByTaxonomyID/${id}`, optionGet)
  .then((response) => {
      return response;
  });
};

export const GetTaxonomiesByParentID = (id) => {
  return fetchApi(`${url}/Api/Taxonomy/a/GetTaxonomiesByParentID/${id}`, optionGet)
  .then((response) => {
      return response;
  });
};

export const GetProceduresByTaxonomyID = (id) => {
  return fetchApi(`${url}/Api/Taxonomy/a/GetProceduresByTaxonomyID/${id}`, optionGet)
  .then((response) => {
      return response;
  });
};

export const GetTaxonomysByProcedureIDs = (list) => {
  const data = list;
  return fetchApi(`${url}/Api/Taxonomy/a/GetTaxonomiesByTaxonomyIDs`, {
    ...optionPost,
    body: JSON.stringify(data)
  })
  .then((response) => {
      return response;
  });
};

export const GetTaxonomyIDsByProcedureIDs = (list) => {
  const data = list;
  return fetchApi(`${url}/Api/Taxonomy/a/GetTaxonomyIDsByProcedureIDs/`, {
    ...optionPost,
    body: JSON.stringify(data)
  })
  .then((response) => {
      return response;
  });
};

export const SearchV3 = (obj) => {
  return fetchApi(`${url}/Api/Procedure/a/SearchV3/`, {
    ...optionPost,
    body: JSON.stringify(obj)
  })
  .then((response) => {
      return response;
  });
};