import {date} from 'quasar';

export function getInitials(fullName: string | null) {
  if (fullName == null) return;
  const allNames = fullName.trim().split(' ');
  const initials = allNames.reduce((acc, curr, index) => {
    if (index === 0 || index === allNames.length - 1) {
      acc = `${acc}${curr.charAt(0).toUpperCase()}`;
    }
    return acc;
  }, '');
  return initials;
}


export function firstToUpperCase(value: string | null) {
  return value && value.length > 0 ? value.charAt(0).toUpperCase() + value.slice(1) : value;
}

export function toSlug(value: string) {
  return value.replace(/[^a-z0-9]/gi, '_').toLowerCase();
}

export function getDatetimeStamp() {
  return date.formatDate(new Date(), 'YYYY-MM-DD-HH:mm:ss');
}
