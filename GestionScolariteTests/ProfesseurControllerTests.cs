using GestionScolarite.ControlLayer;
using GestionScolarite.DataAccessLayer.DAO.Interfaces;
using GestionScolarite.ModelLayer;
using GestionScolarite.ViewLayer;
using Moq;

namespace GestionScolariteTests
{
    [TestClass]
    public class ProfesseurControllerTests
    {
      
        private static readonly List<Professeur> ProfesseursFictifs = new List<Professeur>
        {
            new Professeur(1, "Mostefaoui", "Farida"),
            new Professeur(2, "Dupont",     "Jean"),
            new Professeur(3, "Martin",     "Marie")
        };

      
        [TestMethod]
        public void ListerProfesseurs_AppelleGetAll_EtPasseLaListeAlaVue()
        {
            // ARRANGE
            var mockDAO = new Mock<IProfesseurDAO>();
            mockDAO.Setup(dao => dao.GetAll()).Returns(ProfesseursFictifs);

            var mockView = new Mock<ProfesseurView>();
            var controller = new ProfesseurController(mockDAO.Object, mockView.Object);

            // ACT
            controller.ListerProfesseurs();

            // ASSERT
            mockDAO.Verify(dao => dao.GetAll(), Times.Once());

            mockView.Verify(
                view => view.AfficherListe(
                    It.Is<List<(int id, string prenom, string nom)>>(liste =>
                        liste.Count == 3 &&
                        liste[0].id == 1 && liste[0].prenom == "Farida" && liste[0].nom == "Mostefaoui" &&
                        liste[1].id == 2 && liste[1].prenom == "Jean" && liste[1].nom == "Dupont" &&
                        liste[2].id == 3 && liste[2].prenom == "Marie" && liste[2].nom == "Martin"
                    )
                ),
                Times.Once()
            );
        }

    
        [TestMethod]
        public void RecupererProfesseur_AvecIdValide_RetourneLeProfesseur()
        {
            var mockDAO = new Mock<IProfesseurDAO>();
            var professeurAttendu = ProfesseursFictifs[0];

            mockDAO.Setup(dao => dao.GetById(1)).Returns(professeurAttendu);

            var mockView = new Mock<ProfesseurView>();
            var controller = new ProfesseurController(mockDAO.Object, mockView.Object);

            Professeur resultat = controller.RecupererProfesseur(1);

            mockDAO.Verify(dao => dao.GetById(1), Times.Once());

            Assert.IsNotNull(resultat);
            Assert.AreEqual(1, resultat.Id);
            Assert.AreEqual("Farida", resultat.Prenom);
            Assert.AreEqual("Mostefaoui", resultat.Nom);

            mockView.Verify(view => view.AfficherMessage("Professeur introuvable."), Times.Never());
        }

  
        [TestMethod]
        public void RecupererProfesseur_AvecIdInvalide_RetourneNullEtAfficheerreur()
        {
            var mockDAO = new Mock<IProfesseurDAO>();
            mockDAO.Setup(dao => dao.GetById(99)).Returns((Professeur)null);

            var mockView = new Mock<ProfesseurView>();
            var controller = new ProfesseurController(mockDAO.Object, mockView.Object);

            Professeur resultat = controller.RecupererProfesseur(99);

            Assert.IsNull(resultat);
            mockDAO.Verify(dao => dao.GetById(99), Times.Once());
            mockView.Verify(view => view.AfficherMessage("Professeur introuvable."), Times.Once());
        }

      
        [TestMethod]
        public void ListerProfesseurs_ListeVide_PasseListeVideAlaVue()
        {
            var mockDAO = new Mock<IProfesseurDAO>();
            mockDAO.Setup(dao => dao.GetAll()).Returns(new List<Professeur>());

            var mockView = new Mock<ProfesseurView>();
            var controller = new ProfesseurController(mockDAO.Object, mockView.Object);

            controller.ListerProfesseurs();

            mockDAO.Verify(dao => dao.GetAll(), Times.Once());
            mockView.Verify(
                view => view.AfficherListe(
                    It.Is<List<(int id, string prenom, string nom)>>(liste => liste.Count == 0)
                ),
                Times.Once()
            );
        }
    }
}
