import { MenuItem } from './menu.model';

export const MENU: MenuItem[] = [
  {
    label: 'Main',
    isTitle: true
  },
  {
    label: 'Dashboard',
    icon: 'home',
    link: '/dashboard'
  },
  {
    label: 'Chat',
    icon: 'inbox',
    link: '/chat'
  },
  {
    label: 'Your Posts',
    icon: 'users',
    link: 'general/myposts'
  },
  {
    label: 'Job Posts',
    icon: 'users',
    link: 'general/allposts'
  },
  {
    label: 'Profile',
    isTitle: true
  },
  {
    label: 'Edit Profile ',
    icon: 'edit',
    link: 'profile/edit'
  },
  {
    label: 'View Profile',
    icon: 'user',
    link: 'profile/detail'
  }
];
