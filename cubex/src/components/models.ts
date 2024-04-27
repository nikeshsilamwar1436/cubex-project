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
