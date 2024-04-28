import { Notify } from 'quasar';
import { AxiosError } from 'axios';
import  { PaginationSearch } from 'components/models';
import { PaginationFilter } from 'components/models';
import { firstToUpperCase } from 'src/utils';
import {api} from 'boot/axios';

// services are not in use but i implemented two ways with apiservices
// and api
export class APIServiceBase {
  public static readonly apiVersion = process.env.CORE_API_VERSION;
  public static readonly apiUrl = process.env.CORE_API;

  constructor() {
  }

  static headers(accessToken: string) {
    return {
      ApiVersion: APIServiceBase.apiVersion,
      Authorization: `Bearer ${accessToken}`
    };
  }

  static notify(message: string) {
    Notify.create(message);
  }

  static notifyPositive(message: string) {
    Notify.create({
      type: 'positive',
      message: message,
    });
  }

  static notifyNegative(err: AxiosError) {
    if (err.response) {
      Notify.create({
        type: 'negative',
        message: err.response.status == 400 ? (err.response?.data as string) : err.message,
      });
    }
  }

  static async apiListWithAgParams<Type>(accessToken: string, params: any, search: PaginationSearch | null, requestType: ListRequestType = ListRequestType.Unknown, path: string): Promise<Type[]> {
    const sort = params.request.sortModel && params.request.sortModel.length > 0 ? params.request.sortModel[0] : null;
    const agFilters = [] as PaginationFilter[];

    if (params.request) {
      for (const property in params.request.filterModel) {
        let type = null;
        let value = null;
        let values = null;
        switch (params.request.filterModel[property].filterType) {
          case 'date':
            value = params.request.filterModel[property].dateFrom;
            params.request.filterModel[property].type;
            break;
          case 'set':
            values = params.request.filterModel[property].values;
            type = 'in';
            break;
          default:
            value = `${params.request.filterModel[property].filter}`;
            type = params.request.filterModel[property].type;
        }

        const f = {
          filter: firstToUpperCase(property),
          value: value,
          values: values,
          filterType: params.request.filterModel[property].filterType,
          type: type,
          static: params.request.filterModel[property].static,
        } as PaginationFilter;

        agFilters.push(f);
      }
    }

    return APIServiceBase.apiList(accessToken, params.api.paginationGetCurrentPage() + 1, params.api.paginationGetPageSize(), sort?.colId, sort?.sort, search, agFilters, requestType, path);
  }

  static async apiList<Type>(
    accessToken: string,
    pageNumber: number,
    pageSize: number,
    sort: string,
    sortDirection: string,
    search: PaginationSearch | null,
    filters: PaginationFilter[] | null,
    requestType: ListRequestType = ListRequestType.Unknown,
    path: string,
  ): Promise<Type[]> {
    return APIServiceBase.apiListPost(
      accessToken,
      {
        pageNumber: pageNumber,
        pageSize: pageSize,
        sort: sort,
        sortDirection: sortDirection,
        search: search,
        filters: filters,
        requestType: requestType,
      },
      path,
    );
  }

  static async apiListPost<Type>(accessToken: string, model: any | null, path: string): Promise<Type[]> {
    return await api
      .post(`${path}`, model, {
        headers: APIServiceBase.headers(accessToken),
      })
      .then((response) => {
        if (response.data.results && response.data.results.length == 0) {
          APIServiceBase.notify('No results found.');
        }

        return response.data;
      })
      .catch((err) => {
        APIServiceBase.notifyNegative(err);
        throw err;
      });
  }

  static async apiGet<Type>(accessToken: string, id: string, path: string): Promise<Type> {
    return await api
      .get(`${path}/${id}`, {
        headers: APIServiceBase.headers(accessToken),
      })
      .then((response) => {
        return response.data;
      })
      .catch((err) => {
        APIServiceBase.notifyNegative(err);
        throw err;
      });
  }

  static async apiAdd<Type>(accessToken: string, model: Type, multipart = false, path: string): Promise<Type> {
    try {
      const headers = {
        ...APIServiceBase.headers(accessToken),
        'Content-Type': multipart ? 'multipart/form-data' : 'application/json'
      };
      const response = await api.post(`${path}`, model, {
        headers: headers,
      });

      return response.data;
    } catch (err: any) {
      APIServiceBase.notifyNegative(err);
      throw err;
    }
  }

  static async apiUpdate<Type>(accessToken: string, model: Type, multipart = false, path: string): Promise<Type> {
    try {
      const headers = {
        ...APIServiceBase.headers(accessToken),
        'Content-Type': multipart ? 'multipart/form-data' : 'application/json'
      };
      const response = await api.put(`${path}`, model, {
        headers: headers,
      });
      return response.data;
    } catch (err: any) {
      APIServiceBase.notifyNegative(err);
      throw err;
    }
  }

  static async apiDelete(accessToken: string, id: string, path: string): Promise<boolean> {
    return await api
      .delete(`${path}/${id}`, {
        headers: APIServiceBase.headers(accessToken),
      })
      .then((response) => {
        return response.data;
      })
      .catch((err) => {
        APIServiceBase.notifyNegative(err);
        throw err;
      });
  }
}

export enum ListRequestType {
  Unknown = 0,
  ByOrganisation = 1,
  GlobalOnly = 2,
  ByOrganisationAndGlobal = 3,
}
