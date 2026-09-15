import Navbar from './components/layout/Navbar';
import Footer from './components/layout/Footer';
import GlobalBackground from './components/layout/GlobalBackground';
import Loader from './components/layout/Loader';
import FloatingWhatsApp from './components/layout/FloatingWhatsApp';
import Hero from './components/sections/Hero/Hero';
import About from './components/sections/About/About';
import Skills from './components/sections/Skills/Skills';
import Experience from './components/sections/Experience/Experience';
import Projects from './components/sections/Projects/Projects';
import Education from './components/sections/Education/Education';
import Contact from './components/sections/Contact/Contact';
import './styles/app.css';

export default function App() {
  return (
    <div className="app-shell">
      <Loader />
      <GlobalBackground />
      <Navbar />
      <main className="app-main">
        <Hero />
        <About />
        <Skills />
        <Experience />
        <Projects />
        <Education />
        <Contact />
      </main>
      <Footer />
      <FloatingWhatsApp />
    </div>
  );
}