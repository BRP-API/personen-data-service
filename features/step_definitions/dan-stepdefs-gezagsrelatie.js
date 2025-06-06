const { Then } = require('@cucumber/cucumber');
const { createCollectieObjectMetSubCollectieObject,
        createCollectieObjectMetSubCollectieObjecten,
        createSubCollectieObjectInLastCollectieObject,
        createSubCollectieObjectenInLastCollectieObject,
        createSubSubCollectieObjectInLastSubCollectieObjectInLastCollectieObject,
        createSubSubCollectieObjectenInLastSubCollectieObjectInLastCollectieObject } = require('./dataTable2ObjectFactory');
const { setObjectPropertiesFrom } = require('./dataTable2Object');
const { getBsn,
        getPersoon } = require('./contextHelpers');
const { createPersoonMetGezag, createGezag } = require('./gezagsrelatie');

Then(/^heeft de response een persoon met een 'gezag' met ?(?:alleen)? de volgende gegevens$/, function (dataTable) {
    this.context.verifyResponse = true;

    createCollectieObjectMetSubCollectieObject(this.context, 'persoon', 'gezag', dataTable);
});

Then(/^heeft de persoon ?(?:nog)? een 'gezag' met ?(?:alleen)? de volgende gegevens$/, function (dataTable) {
    createSubCollectieObjectInLastCollectieObject(this.context, 'persoon', 'gezag', dataTable);
});

Then(/^heeft ?(?:het)? 'gezag' ?(?:nog)? een '(\w*)' met de volgende gegevens$/, function (relatie, dataTable) {
    createSubSubCollectieObjectInLastSubCollectieObjectInLastCollectieObject(this.context, 'persoon', 'gezag', relatie, dataTable);
});

Then(/^heeft de response een persoon zonder gezag$/, function () {
    this.context.verifyResponse = true;

    createCollectieObjectMetSubCollectieObjecten(this.context, 'persoon', 'gezag');
});

Then(/^heeft de persoon geen gezag$/, function () {
    createSubCollectieObjectenInLastCollectieObject(this.context, 'persoon', 'gezag');
});

Then(/^heeft ?(?:het)? 'gezag' geen derden$/, function () {
    createSubSubCollectieObjectenInLastSubCollectieObjectInLastCollectieObject(this.context, 'persoon', 'gezag', 'derde');
});

function initExpected(context, type, aanduidingMinderjarige, aanduidingMeerderjarige1, aanduidingMeerderjarige2, toelichting = undefined) {
    context.verifyResponse = true;

    if(!context.expected) {
        context.expected = {
            personen: []
        };
    }

    const expectedPersoon = context.expected.personen.at(-1);
    if(!expectedPersoon) {
        context.expected.personen.push(createPersoonMetGezag(context, type, aanduidingMinderjarige, aanduidingMeerderjarige1, aanduidingMeerderjarige2, toelichting));
    }
    else {
        expectedPersoon.gezag.push(createGezag(context, type, aanduidingMinderjarige, aanduidingMeerderjarige1, aanduidingMeerderjarige2, toelichting));
    }
}

Then(/^(?:is )?het gezag over '([a-zA-Z0-9À-ž-]*)' (?:is )?(eenhoofdig ouderlijk gezag|gezamenlijk gezag) met ouder '([a-zA-Z0-9À-ž-]*)'(?: en een onbekende derde)?$/, function (aanduidingMinderjarige, type, aanduidingOuder) {
    initExpected(this.context, type, aanduidingMinderjarige, aanduidingOuder);
});

Then(/^(?:is )?het gezag over '([a-zA-Z0-9À-ž-]*)' (?:is )?(gezamenlijk gezag|gezamenlijk ouderlijk gezag) met ouder '([a-zA-Z0-9À-ž-]*)' en (?:ouder|derde) '([a-zA-Z0-9À-ž-]*)'$/, function (aanduidingMinderjarige, type, aanduidingMeerderjarige1, aanduidingMeerderjarige2) {
    initExpected(this.context, type, aanduidingMinderjarige, aanduidingMeerderjarige1, aanduidingMeerderjarige2);
});
       
Then(/^(?:is )?het gezag over '([a-zA-Z0-9À-ž-]*)' (?:is )?voogdij(?: met derde '([a-zA-Z0-9À-ž-]*)')?$/, function (aanduidingMinderjarige, aanduidingMeerderjarige) {
    initExpected(this.context, 'voogdij', aanduidingMinderjarige, aanduidingMeerderjarige);
});

Then('is het gezag over {aanduiding} {tijdelijk geen gezag of niet te bepalen} met de toelichting {toelichting}', function (aanduidingMinderjarige, type, toelichting) {
    initExpected(this.context, type, aanduidingMinderjarige, undefined, undefined, toelichting);
});

Then(/^heeft de (minderjarige|ouder|derde) de volgende gegevens$/, function (type, dataTable) {
    let expected = this.context.expected.personen.at(-1).gezag.at(-1);

    setObjectPropertiesFrom(expected[type], dataTable);
});

Then(/^heeft de (minderjarige|ouder|derde) geen (\w*)$/, function (type, property) {
    let expected = this.context.expected.personen.at(-1).gezag.at(-1);

    if(expected[type][property]) {
        delete expected[type][property];
    }
});

Then('heeft {aanduiding} geen gezaghouder', function (aanduidingMinderjarige) {
    this.context.verifyResponse = true;

    const expected = {
        personen: [
            {
                gezag: []
            }
        ]
    };

    if(this.context.isGezagApiAanroep) {
        expected.personen[0].burgerservicenummer = getBsn(getPersoon(this.context, aanduidingMinderjarige));
    }

    this.context.expected = expected;
});

Then('is het gezag in onderzoek', function () {
    this.context.expected.personen.at(-1).gezag.at(-1).inOnderzoek = "true";
});

Then('heeft {aanduiding} de volgende gezagsrelaties', function (aanduiding) {
    this.context.verifyResponse = true;

    if(!this.context.expected) {
        this.context.expected = {
            personen: []
        };
    }

    let persoon = {
        gezag: []
    }
    if(this.context.isGezagApiAanroep) {
        persoon.burgerservicenummer = getBsn(getPersoon(this.context, aanduiding));
    }
    this.context.expected.personen.push(persoon);
});
