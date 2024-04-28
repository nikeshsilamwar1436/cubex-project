export interface menuInter {
  icon: string;
  label: string;
  link: string;
}

export interface tableColumns{
  name: string | null,
  label: string | null,
  align: string,
  field: string | null,
  sortable: boolean
}

export interface PaginationFilter {
  filter: string | null;
  value: string | null;
  filterType: string | null;
  type: string | null;
  static: boolean | null;
}

export interface PaginationSearch {
  search: string[] | null
  value: string | null
}
