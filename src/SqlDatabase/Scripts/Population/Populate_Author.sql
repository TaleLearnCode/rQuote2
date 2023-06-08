MERGE INTO dbo.Author AS TARGET
USING (VALUES ('steven-wright',      'Steven Wright',      'Steven Write is an American stand-up comedian, actor, write, and film producter known for his ditinctly lethargic voice and slow, deadpan delivery of ironic, philosophical and sometimes nonsensical jokes, paraprosdokians, non sequiturs, anti-humor, and one-liners with contrived situations.'),
              ('george-carlin',      'George Carlin',      'George Carlin was an American comedian, actor, author, and social critic known for his black comedy and reflections on polics, the English language, psychology, religion, and taboo subjects.'),
              ('bob-newhart',        'Bob Newhart',        'Bob Newhart is an American comedian and actor known for his deadpan and stammering delivery style.'),
              ('bernie-mac',         'Bernie Mac',         'Bernie Mac was an American comedian and actor.'),
              ('redd-foxx',          'Redd Foxx',          'Redd Foxx was an American stand-up comedian and actor who gained success with his raunchy nighclub act before and during the civil rights movementt.  He is best known for his portrayal of Fed G. Sanford on the television show Sanford and Son.'),
              ('rodney-dangerfield', 'Rodney Dangerfield', 'Rodney Dangerfield was an American stand-up comedian, actor, screenwriter, and producer known for his self-deprecating one-liner homor, his catchphrase "I don''t get no respect!", and his monologues on that theme.'),
              ('bill-burr',          'Bill Burr',          'Bill Burr is an American stand-up comedian, actor, filmmaker, and podcaster.'),
              ('steve-martin',       'Steven Martin',      'Steve Martin is an American actor, comedian, writer, producter, and musician.'),
              ('robin-williams',     'Robin Williams',     'Robin Williams was an American actor and comedian known for his improvisational skills and the wide variety of characters he created on the spur of the moment and portrayed on film, in drams and comedians alike.  He is regarded as one of the greatest comedians of all time.'),
              ('joel-spolsky',       'Joel Spolsky',       'Joel Spolsky is a software engineer and a writer best known for his Joel on Software blog on software development, being the creator of Trello, and launching Stack Overflow.'),
              ('alan-turing',        'Alan Turing',        'Alan Turing was an English mathematician, computer scientist, logician, cryptanalyst, philospher, and theoretical biologist widly considered to be the father of theoretical computer science and artificial intelligence.'),
              ('linus-torvalds',     'Linus Torvalds',     'Linus Torvalds is a Finish softare engineer who is the creator and, historically, the lead developer of the Linux kernel, used by Linux distributions and other operating systems such as Android.  He also created the distributed version control system Git.'),
              ('dolly-parton',       'Dolly Parton',       'Dolly Parton is an American singer-songwriter, actress, philanthropist, and businesswoman, known primarily for her decades-long career in country music.'))
AS SOURCE (AuthorId,
           AuthorName,
           Bio)
ON TARGET.AuthorId = SOURCE.AuthorId
WHEN MATCHED THEN UPDATE SET TARGET.AuthorName = SOURCE.AuthorName,
                             TARGET.Bio        = SOURCE.Bio
WHEN NOT MATCHED THEN INSERT (AuthorId,
                              AuthorName,
                              Bio)
                      VALUES (SOURCE.AuthorId,
                              SOURCE.AuthorName,
                              SOURCE.Bio);
GO