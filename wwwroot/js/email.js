// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

document.addEventListener('DOMContentLoaded', function () {
    let activeEmail = null;

    const emailItems = document.querySelectorAll('.email-item');
    const emailContentArea = document.querySelector('#email-content');
    const aiResponseArea = document.querySelector('#ai-response');

    // Ajouter un écouteur sur le bouton "Générer une réponse"
    document.addEventListener('click', function (e) {
        if (e.target && e.target.matches('#btn-generate'))
        {
            if (!activeEmail)
            {
                alert('Veuillez sélectionner un email avant de générer une réponse');
                return;
            }
            let inputValue = document.getElementById("textarea-prompt").value;
            // Envoyer une requête POST au backend
            fetch('/OpenAI/SendAnswer', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({ 
                    EmailContent: activeEmail.Content,
                    PromptContent: inputValue})
                })
                .then(response => response.json())
                .then(data => {
                    aiResponseArea.innerHTML = `
                        <h3>Réponse suggérée</h3>
                        <div class="ai-response">
                            <div class="ai-response-header">
                                <div class="ai-icon">AI</div>
                                <strong>Réponse générée:</strong>
                            </div>
                             <pre>${data.response}</pre>
                             <textarea id="textarea-prompt" placeholder="(Optionnel) Ecrivez un prompt. Ex : 'répond positivement au mail mais dis que nous sommes fermés pendant les fêtes.'"></textarea>
                        </div>

                        <div class="action-buttons">
                            <button id="btn-generate" class="btn btn-primary">Générer email</button>
                            <button id="btn-envoyer" class="btn btn-secondary">Envoyer</button>
                        </div>
                    `;
                })
                .catch(error => {
                    console.error('Erreur lors de l\'envoi de la requête :', error);
                    aiResponseArea.innerHTML = `<p>Erreur lors de la génération de la réponse.</p>`;
                });
        }
        if (e.target && e.target.matches("#btn-envoyer"))
        {
            alert("Cette fonctionnalité n'a pas encore été integrée");
        }
    });

    emailItems.forEach(item => {
        item.addEventListener('click', function () {
            // Retirer la classe "active" de tous les emails
            emailItems.forEach(email => email.classList.remove('active'));
            // Ajouter la classe "active" sur l'email cliqué
            this.classList.add('active');

            // Récupérer l'ID de l'email
            const emailId = this.getAttribute('data-email-id');
            activeEmail = emailData.find(e => e.EmailId === emailId);
            if (activeEmail) {
                // Mettre à jour l'affichage
                emailContentArea.innerHTML = `
                    <div class="email-header">
                        <h2>${activeEmail.Subjects}</h2>
                        <div class="email-meta">
                            De: ${activeEmail.Sender}<br />
                            Date: ${new Date().toLocaleString('fr-FR')}
                        </div>
                    </div>
                    <pre>${activeEmail.Content}</pre>
                `;
                aiResponseArea.innerHTML = `
                    <h3>Réponse suggérée</h3>
                    <div class="ai-response">
                        <div class="ai-response-header">
                            <div class="ai-icon">AI</div>
                            <strong>Réponse générée:</strong>
                        </div>
                         <pre>Aucune email généré pour le moment. Cliquez sur le bouton "Générer email" pour générer un mail.</pre>
                         <textarea id="textarea-prompt" placeholder="(Optionnel) Ecrivez un prompt. Ex : 'répond positivement au mail mais dis que nous sommes fermés pendant les fêtes.'"></textarea>
                    </div>

                    <div class="action-buttons">
                        <button id="btn-generate" class="btn btn-primary">Générer email</button>
                        <button id="btn-envoyer" class="btn btn-secondary">Envoyer</button>
                    </div>
                    `;
            } else {
                emailContentArea.innerHTML = `<p>Aucun contenu disponible pour cet email.</p>`;
            }
        });
    });
});