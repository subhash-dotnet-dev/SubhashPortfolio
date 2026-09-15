export interface Profile {
  id: string;
  fullName: string;
  title: string;
  email: string;
  phone: string;
  location: string;
  profileImageUrl: string | null;
  resumeUrl: string | null;
  shortBio: string;
  professionalSummary: string;
  careerDirection: string;
  isAvailableForHire: boolean;
  createdAt: string;
  updatedAt: string | null;
}

export interface SocialLink {
  id: string;
  platform: number;
  displayName: string;
  url: string;
  iconClass: string | null;
  displayOrder: number;
  isVisible: boolean;
}

export interface Skill {
  id: string;
  name: string;
  category: number;
  proficiency: number;
  yearsOfExperience: number;
  displayOrder: number;
  isVisible: boolean;
}

export interface ExperienceResponsibility {
  id: string;
  description: string;
  displayOrder: number;
}

export interface Experience {
  id: string;
  company: string;
  role: string;
  location: string;
  startDate: string;
  endDate: string | null;
  isCurrent: boolean;
  description: string;
  techEnvironment: string;
  displayOrder: number;
  responsibilities: ExperienceResponsibility[];
}

export interface ProjectFeature {
  id: string;
  description: string;
  displayOrder: number;
}

export interface ProjectTechnology {
  id: string;
  name: string;
  displayOrder: number;
}

export interface Project {
  id: string;
  title: string;
  subtitle: string;
  shortDescription: string;
  problem: string;
  solution: string;
  architecture: string;
  securityNotes: string | null;
  engineeringChallenges: string;
  results: string;
  githubUrl: string | null;
  liveDemoUrl: string | null;
  thumbnailUrl: string | null;
  status: number;
  displayOrder: number;
  isFeatured: boolean;
  features: ProjectFeature[];
  technologies: ProjectTechnology[];
}

export interface Education {
  id: string;
  degree: string;
  institution: string;
  board: string | null;
  level: number;
  startYear: number | null;
  completionYear: number;
  gradeOrPercentage: string;
  description: string | null;
  displayOrder: number;
}

export interface ContactFormData {
  name: string;
  email: string;
  phone: string;
  subject: string;
  message: string;
}

export interface ContactResponse {
  success: boolean;
  message: string;
}
