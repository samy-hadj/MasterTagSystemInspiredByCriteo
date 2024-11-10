export interface TagModel {
  id: string;
  destinationUrl: string;
  trackingData: string;
  clickCount?: number;       // Compteur de clics, optionnel
  sessionId?: string;        // Identifiant de session, optionnel
  referrer?: string;         // URL de référence, optionnel
  details?: Details;         // Champ imbriqué pour les informations détaillées
}

export interface Details {
  userInfo?: UserInfo;       // Informations utilisateur
  activity?: Activity;       // Informations d'activité utilisateur
}

export interface UserInfo {
  age: number;               // Âge de l'utilisateur
  location?: string;         // Localisation de l'utilisateur, optionnel
  preferences?: Preferences; // Préférences utilisateur, optionnel
}

export interface Preferences {
  theme?: string;            // Préférence de thème (dark ou light)
  language?: string;         // Langue préférée
}

export interface Activity {
  lastLogin?: string;        // Dernière connexion de l'utilisateur, formatée en chaîne de date
  pagesVisited: number;      // Nombre de pages visitées
  actions?: string;          // Actions réalisées par l'utilisateur (clic, scroll, etc.), optionnel
}
